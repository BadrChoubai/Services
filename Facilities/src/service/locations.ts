import { Location } from "../models/Location.js";
import { DatabaseSync } from "node:sqlite";
import database from "../database/sqlite.js";

export const inMemoryLocationsStore: Location[] = [
  {
    id: "f103279-fe1c-4d68-8043-09ad13f13207",
    address: "8350 Quintero St",
    city: "Commerce City",
    stateCode: "CO",
    postalCode: "80022",
    countryCode: "US",
  },
];

export class LocationsService {
  private _db: DatabaseSync;

  constructor(db: DatabaseSync) {
    this._db = db;
  }

  async getAll(): Promise<never> {
    const query = database.prepare(`SELECT * FROM locations`);

    console.log(query.all());
    return;
  }
}
