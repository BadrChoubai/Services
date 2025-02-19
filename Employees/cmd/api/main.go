package main

import (
	"context"
	"errors"
	"fmt"
	"github.com/badrchoubai/Services/Employees/api"
	"github.com/badrchoubai/Services/Employees/pkg/logging"
	"io"
	"log"
	"net/http"
	"os"
	"os/signal"
	"sync"
	"syscall"
	"time"
)

func main() {
	ctx := context.Background()
	if err := run(ctx, os.Stdout, os.Getenv); err != nil {
		log.Fatalf("%+v\n", err)
	}
}

func run(ctx context.Context, stdout io.Writer, getenv func(string) string) error {
	var wg sync.WaitGroup

	logger := logging.NewLogger(stdout)
	handler := api.NewServer(logger)

	srv := &http.Server{
		Addr:    ":" + getenv("PORT"),
		Handler: handler,
	}

	shutdownError := make(chan error)

	go func() {
		quit := make(chan os.Signal, 1)
		signal.Notify(quit, syscall.SIGINT, syscall.SIGTERM)
		s := <-quit

		logger.Info(fmt.Sprintf("caught signal: %s", s))

		shutdownCtx, cancel := context.WithTimeout(ctx, 30*time.Second)
		defer cancel()

		err := srv.Shutdown(shutdownCtx)
		if err != nil {
			shutdownError <- err
		}

		logger.Info("completing background tasks")

		wg.Wait()
		shutdownError <- nil
	}()

	logger.Info(fmt.Sprintf("http://localhost:%s", getenv("PORT")))

	err := srv.ListenAndServe()
	if !errors.Is(err, http.ErrServerClosed) {
		return err
	}

	err = <-shutdownError
	if err != nil {
		return err
	}

	return nil
}
