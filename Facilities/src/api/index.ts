import { Router } from "express";

import facilitiesRoutes from "../handlers/facilities/index.js";
import {
  FacilitiesService,
  inMemoryFacilitiesStore,
} from "../services/facilities.js";

const router = Router();

const facilitiesService = new FacilitiesService(inMemoryFacilitiesStore);

facilitiesRoutes(router, facilitiesService);

export default router;
