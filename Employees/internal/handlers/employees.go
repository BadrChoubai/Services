package handlers

import (
  "database/sql"
  "net/http"
)

func GetEmployees(db *sql.DB) http.Handler {
  return http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
    w.Header().Set("Content-Type", "application/json; charset=utf-8")
    w.WriteHeader(http.StatusOK)
    w.Write([]byte(`{"data": []}`))
  })
}
