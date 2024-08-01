package main

import (
	"context"
	"errors"
	"log"
	"net"
	"net/http"
	"os"
	"os/signal"
	"services-events/internal/config"
	"services-events/internal/router"
	"strconv"
	"sync"
	"time"
)

func run(ctx context.Context, cfg *config.Config) error {
	wg := &sync.WaitGroup{}
	wg.Add(1)

	ctx, cancel := signal.NotifyContext(ctx, os.Interrupt)
	defer cancel()

	handlerMux := router.NewRouter()

	srv := &http.Server{
		Addr:         net.JoinHostPort("0.0.0.0", strconv.Itoa(cfg.Port)),
		Handler:      handlerMux,
		ReadTimeout:  5 * time.Second,
		WriteTimeout: 10 * time.Second,
		IdleTimeout:  time.Minute,
	}

	go func() {
		log.Printf("listening on http://%s\n", srv.Addr)
		if err := srv.ListenAndServe(); err != nil && !errors.Is(err, http.ErrServerClosed) {
			log.Panicf("error listening and serving: %s", err)
		}
	}()

	go func() {
		defer wg.Done()
		<-ctx.Done()

		shutdownCtx := context.Background()
		shutdownCtx, cancel = context.WithTimeout(shutdownCtx, 30*time.Second)
		defer cancel()

		if err := srv.Shutdown(shutdownCtx); err != nil && !errors.Is(err, http.ErrServerClosed) {
			log.Fatalf("failed to start server: %v", err)
		}
	}()

	wg.Wait()
	return nil
}

func main() {
	errorLog := log.New(os.Stderr, "ERROR ", log.LstdFlags)

	ctx := context.Background()
	cfg := config.Load()

	if cfg == nil {
		errorLog.Fatal("failed to load configuration")
	}

	if err := run(ctx, cfg); err != nil {
		errorLog.Fatalf("failed to run server: %v", err)
	}
}
