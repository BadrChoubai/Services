CREATE TABLE IF NOT EXISTS employees
(
  id          TEXT PRIMARY KEY,
  first_name  TEXT NOT NULL,
  last_name   TEXT NOT NULL,
  facility_id TEXT NOT NULL,
  FOREIGN KEY (facility_id) REFERENCES facilities (id) ON DELETE CASCADE
)
