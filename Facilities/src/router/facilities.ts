import { Router } from "express";
import database from "../database/sqlite.js";

import { getFacilities } from "./handlers/getFacilities.js";
import { getFacility } from "./handlers/getFacility.js";

import { FacilitiesService } from "../service/facilities.js";

const facilities = Router();
const facilitiesService = new FacilitiesService(database);

// Entire API surface mapped here
facilities.get("/", getFacilities(facilitiesService));
facilities.get("/:id", getFacility(facilitiesService));

export default facilities;
