package sqlite

import (
  "context"
  "database/sql"
  "time"
)

const (
  maxOpenConns = 5
  maxLifetime  = 120
  maxIdleConns = 2
  maxIdleTime  = 20
)

func Open() (*sql.DB, error) {
  db, err := sql.Open("postgres", cfg.db.dsn)
  if err != nil {
    return nil, err
  }

  db.SetConnMaxLifetime(maxLifetime)
  db.SetMaxOpenConns(maxOpenConns)
  db.SetMaxIdleConns(maxIdleConns)
  db.SetConnMaxIdleTime(maxLifetime)

  ctx, cancel := context.WithTimeout(context.Background(), 5*time.Second)
  defer cancel()

  err = db.PingContext(ctx)
  if err != nil {
    return nil, err
  }

  return db, nil
}
