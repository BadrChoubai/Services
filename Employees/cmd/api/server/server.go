package server

import (
  "context"
  "github.com/badrchoubai/Services/Employees/internal/routes"
  "github.com/badrchoubai/Services/Employees/pkg/logging"
  "github.com/badrchoubai/Services/Employees/pkg/middleware"
  "net/http"
)

var _ HTTPServer = (*Server)(nil)

type HTTPServer interface {
  ListenAndServe() error
  Shutdown(context.Context) error
}

type Server struct {
  HttpServer *http.Server
  logger     *logging.Logger
}

func NewServer(logger *logging.Logger, getEnv func(string) string) *Server {
  port := getEnv("HTTP_PORT")

  srv := &Server{
    //HttpServer: createStdLibHTTPServer(port),
    HttpServer: &http.Server{
      Addr:    ":" + port,
      Handler: setupHandler(logger),
    },
    logger: logger,
  }

  return srv
}

func setupHandler(logger *logging.Logger) http.Handler {
  mux := http.NewServeMux()
  addRoutes(mux)

  var handler http.Handler = mux
  handler = middleware.Heartbeat(handler, "/health")
  handler = middleware.LogRequest(handler, logger)

  return handler
}

func addRoutes(mux *http.ServeMux) {
  mux.Handle("/", routes.RootHandler())
}

func (s *Server) ListenAndServe() error {
  s.logger.Info("Starting HTTP Server")
  s.logger.Info("http://localhost" + s.HttpServer.Addr)
  return s.HttpServer.ListenAndServe()
}

func (s *Server) Shutdown(ctx context.Context) error {
  s.logger.Info("Shutting down HTTP Server")
  return s.HttpServer.Shutdown(ctx)
}
