import { Router } from "express";

import { getFacilityById } from "./getFacilityById.js";
import { getFacilities } from "./getFacilities.js";

import { FacilitiesService } from "../../services/index.js";

export default (router: Router, facilitiesService: FacilitiesService) => {
  const facilities = Router();

  // Entire API surface mapped here
  facilities.get("/", getFacilities(facilitiesService));
  facilities.get("/:id", getFacilityById(facilitiesService));

  // Mount routes to application router
  router.use("/facilities", facilities);
};
