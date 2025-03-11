import database from "./sqlite.js";
import { Facility, FacilityType } from "../models/Facility.js";
import { Location } from "../models/Location.js";

database.exec(`
  DROP TABLE IF EXISTS locations
`);
database.exec(`
  DROP TABLE IF EXISTS facilities
`);

database.exec(`
  CREATE TABLE IF NOT EXISTS locations
  (
    id           TEXT PRIMARY KEY,
    address      TEXT NOT NULL,
    city         TEXT NOT NULL,
    state_code   TEXT NOT NULL,
    postal_code  TEXT NOT NULL,
    country_code TEXT NOT NULL
  )
`);

database.exec(`
  CREATE TABLE IF NOT EXISTS facilities
  (
    id          TEXT PRIMARY KEY,
    alias       TEXT NOT NULL,
    location_id TEXT NOT NULL,
    type        INT NOT NULL,
    FOREIGN KEY (location_id) REFERENCES locations (id) ON DELETE CASCADE
  )
`);

const locations: Location[] = [
  {
    id: "f103279-fe1c-4d68-8043-09ad13f13207",
    address: "8350 Quintero St",
    city: "Commerce City",
    stateCode: "CO",
    postalCode: "80022",
    countryCode: "US",
  },
];

const facilities: Facility[] = [
  {
    id: "1e95757d-066d-41a6-b5d2-5e34b71d6051",
    alias: "BDU5",
    locationId: locations[0].id,
    type: FacilityType.SortCenter,
  },
];

const insertLocation = database.prepare(`
  INSERT INTO locations (id, address, city, state_code, postal_code,
                         country_code)
  VALUES (?, ?, ?, ?, ?, ?)
`);

const insertFacility = database.prepare(`
  INSERT INTO facilities (id, alias, location_id, type)
  VALUES (?, ?, ?, ?)
`);

locations.forEach((location) => {
  insertLocation.run(
    location.id,
    location.address,
    location.city,
    location.stateCode,
    location.postalCode,
    location.countryCode,
  );
});

facilities.forEach((facility) => {
  insertFacility.run(
    facility.id,
    facility.alias,
    facility.locationId,
    facility.type,
  );
});
