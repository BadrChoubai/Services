package handlers

import (
	"net/http"

	"github.com/badrchoubai/Services/Employees/internal/data"
	"github.com/badrchoubai/Services/Employees/internal/repository"
	"github.com/badrchoubai/Services/Employees/pkg/encoding"
)

func GetEmployees(employeesRepository *repository.EmployeesRepository) http.Handler {
	type ListResponse struct {
		Employees []*data.Employee `json:"employees"`
		Count     int              `json:"count"`
	}

	return http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		employees, err := employeesRepository.GetAll()
		if err != nil {
			http.Error(w, err.Error(), http.StatusInternalServerError)
			return
		}
		response := &ListResponse{
			Employees: employees,
			Count:     len(employees),
		}

		ec := encoding.NewEncoderDecoder()

		err = ec.EncodeResponse(w, http.StatusOK, response)
		if err != nil {
			http.Error(w, err.Error(), http.StatusInternalServerError)
			return
		}
	})
}
