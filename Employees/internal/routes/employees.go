package routes

import (
	"database/sql"
	"github.com/badrchoubai/Services/Employees/internal/handlers"
	"github.com/badrchoubai/Services/Employees/internal/repository"
	"net/http"
)

func NewEmployeesRouter(mux *http.ServeMux, db *sql.DB) {
	employeesRepository := repository.NewEmployeesRepository(db)

	addRoutes(mux, employeesRepository)
}

func addRoutes(mux *http.ServeMux, employeesRepository *repository.EmployeesRepository) {
	mux.Handle("/employees", handlers.GetEmployees(employeesRepository))
}
