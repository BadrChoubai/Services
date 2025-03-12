package routes

import (
	"database/sql"
	"github.com/badrchoubai/Services/Employees/internal/handlers"
	"net/http"
)

type EmployeesRouter struct {
	Handler *http.ServeMux
	router  http.Handler
	db      *sql.DB
}

func NewEmployeesRouter(mux *http.ServeMux, db *sql.DB) *EmployeesRouter {
	addRoutes(mux, db)

	return &EmployeesRouter{
		Handler: mux,
	}
}

func addRoutes(mux *http.ServeMux, database *sql.DB) {
	mux.Handle("/employees", handlers.GetEmployees(database))
}
