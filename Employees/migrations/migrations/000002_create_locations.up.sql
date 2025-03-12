CREATE TABLE IF NOT EXISTS locations
(
  id           TEXT PRIMARY KEY,
  address      TEXT NOT NULL,
  city         TEXT NOT NULL,
  state_code   TEXT NOT NULL,
  postal_code  TEXT NOT NULL,
  country_code TEXT NOT NULL
)
