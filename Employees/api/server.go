package api

import (
	"github.com/badrchoubai/Services/Employees/pkg/logging"
	"github.com/badrchoubai/Services/Employees/pkg/middleware"
	"net/http"
)

func NewServer(logger *logging.Logger) http.Handler {
	mux := http.NewServeMux()
	addRoutes(mux, logger)

	var handler http.Handler = mux
	handler = middleware.Heartbeat(handler, "/health")
	handler = middleware.LogRequest(handler, logger)

	return handler
}
