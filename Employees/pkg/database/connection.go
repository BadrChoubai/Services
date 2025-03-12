package database

import (
	"context"
	"database/sql"
	"github.com/badrchoubai/Services/Employees/pkg/observability/logging"
	"os"
	"time"

	_ "github.com/mattn/go-sqlite3"
)

const (
	maxOpenConns = 5
	maxLifetime  = 120
	maxIdleConns = 2
	maxIdleTime  = 20
)

func Open() (*sql.DB, error) {
	logger := logging.NewLogger(os.Stdout)
	db, err := sql.Open("sqlite3", ".db/Employees.db")
	if err != nil {
		logger.Error("opening database", err)
		return nil, err
	}

	db.SetConnMaxLifetime(maxLifetime)
	db.SetMaxOpenConns(maxOpenConns)
	db.SetMaxIdleConns(maxIdleConns)
	db.SetConnMaxIdleTime(maxIdleTime)

	ctx, cancel := context.WithTimeout(context.Background(), 5*time.Second)
	defer cancel()

	err = db.PingContext(ctx)
	if err != nil {
		return nil, err
	}

	return db, nil
}
