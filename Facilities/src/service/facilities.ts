import { DatabaseSync } from "node:sqlite";

export class FacilitiesService {
  private _db: DatabaseSync;

  constructor(db: DatabaseSync) {
    this._db = db;
  }

  async getAll(): Promise<unknown[]> {
    const query = this._db.prepare(`
      SELECT f.id AS id, f.alias, f.type,
             l.id AS locationId, l.address, l.city, l.country_code AS countryCode, l.postal_code AS postalCode, l.state_code AS stateCode
      FROM facilities as f
             LEFT JOIN locations AS l ON f.location_id = l.id
    `);

    return query.all();
  }

  async getOne(id: string): Promise<unknown> {
    const query = this._db.prepare(`
      SELECT f.id AS id, f.alias, f.type,
             l.id AS locationId, l.address, l.city, l.country_code AS countryCode, l.postal_code AS postalCode, l.state_code AS stateCode
      FROM facilities as f
             LEFT JOIN locations AS l ON f.location_id = l.id
        WHERE f.id = ?
    `);

    return query.get(id);
  }
}
