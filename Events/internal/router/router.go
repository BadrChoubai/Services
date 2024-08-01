package router

import (
	"github.com/go-chi/chi/middleware"
	"services-events/internal/router/handlers"
	"time"

	"github.com/go-chi/chi"
)

func NewRouter() *chi.Mux {
	r := chi.NewRouter()

	r.Use(middleware.RequestID)
	r.Use(middleware.RealIP)
	r.Use(middleware.Logger)
	r.Use(middleware.Recoverer)

	r.Use(middleware.Timeout(60 * time.Second))

	r.Get("/health", handlers.Healthcheck())

	r.MethodNotAllowed(handlers.MethodNotAllowed)
	r.NotFound(handlers.NotFound)

	return r
}
