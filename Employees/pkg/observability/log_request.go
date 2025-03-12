package observability

import (
	"github.com/badrchoubai/Services/Employees/pkg/observability/logging"
	"net/http"
)

func LogRequest(handler http.Handler, logger *logging.Logger) http.Handler {
	return http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		logger.Info("request",
			"method", r.Method,
			"path", r.URL.Path,
		)

		handler.ServeHTTP(w, r)
	})
}
