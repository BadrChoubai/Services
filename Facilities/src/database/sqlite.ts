import { DatabaseSync } from "node:sqlite";

const database: DatabaseSync = new DatabaseSync(".db/Facilities.db");

export default database;
