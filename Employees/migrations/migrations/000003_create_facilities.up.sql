CREATE TABLE IF NOT EXISTS facilities
(
  id          TEXT PRIMARY KEY,
  alias       TEXT NOT NULL,
  location_id TEXT NOT NULL,
  type        INT NOT NULL,
  FOREIGN KEY (location_id) REFERENCES locations (id) ON DELETE CASCADE
)
