package api

import (
	"github.com/badrchoubai/Services/Employees/pkg/logging"
	"net/http"
)

func addRoutes(mux *http.ServeMux, logger *logging.Logger) {
	mux.Handle("/", http.NotFoundHandler())

	mux.Handle("/employees", http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		w.Header().Set("Content-Type", "application/json; charset=utf-8")
		_, err := w.Write([]byte(`{"employees":[]}`))
		if err != nil {
			logger.Error(err.Error())
			http.Error(w, err.Error(), http.StatusInternalServerError)
			return
		}
	}))
}
