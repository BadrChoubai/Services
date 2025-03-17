package repository

import (
	"context"
	"database/sql"
	"github.com/google/uuid"
	"time"

	"github.com/badrchoubai/Services/Employees/internal/data"
)

var _ Repository = (*EmployeesRepository)(nil)

type Repository interface {
	GetAll() ([]*data.Employee, error)
}
type EmployeesRepository struct {
	db *sql.DB
}

func NewEmployeesRepository(db *sql.DB) *EmployeesRepository {
	return &EmployeesRepository{
		db: db,
	}
}

func (r *EmployeesRepository) GetAll() ([]*data.Employee, error) {
	query := `
SELECT 
    e.id, e.first_name, e.last_name, 
    f.id AS id, f.alias, f.type, 
    l.id AS id, l.address, l.city, l.state_code, l.postal_code, l.country_code
FROM employees e
LEFT JOIN main.facilities f ON e.facility_id = f.id
LEFT JOIN main.locations l ON f.location_id = l.id;

`

	ctx, cancel := context.WithTimeout(context.Background(), 3*time.Second)
	defer cancel()

	rows, err := r.db.QueryContext(ctx, query)
	if err != nil {
		return nil, err
	}

	defer rows.Close()

	var employees []*data.Employee
	var facilityId sql.NullString
	var locationId sql.NullString

	if rows.Next() {
		var employee data.Employee
		var facility data.Facility
		var location data.Location

		err := rows.Scan(
			&employee.Id,
			&employee.FirstName,
			&employee.LastName,
			&facilityId,
			&facility.Alias,
			&facility.Type,
			&locationId,
			&location.Address,
			&location.City,
			&location.CountryCode,
			&location.PostalCode,
			&location.StateCode,
		)
		if err != nil {
			return nil, err
		}

		// Only assign facility if it exists
		if facilityId.Valid {
			facility.Id = uuid.MustParse(facilityId.String)

			// Only assign location if it exists
			if locationId.Valid {
				location.Id = locationId.String
				facility.Location = location
			}
			employee.Facility = facility
		}

		employees = append(employees, &employee)
	}

	return employees, nil
}
