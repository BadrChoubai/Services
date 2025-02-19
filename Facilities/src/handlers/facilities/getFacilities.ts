import { Service } from "../../services/Service.js";
import { Facility } from "../../services/index.js";
import { transformResponse } from "../../lib/transformResponse.js";

import type {
  FacilitiesListResponse,
  FacilityResponse,
} from "../../services/facilities.js";
import { FacilityType } from "../../services/facilities.js";

export const getFacilities = (facilitiesService: Service<Facility>) => {
  return async function (req, res) {
    try {
      const facilities = facilitiesService.getAll();

      const facilitiesListResponse: FacilitiesListResponse = facilities.map(
        (facility: Facility): FacilityResponse =>
          transformResponse<Facility, FacilityResponse>(
            facility,
            toFacilityResponse,
          ),
      );

      const data = {
        facilities: facilitiesListResponse || [],
      };

      res.status(200).send(data);
    } catch (error) {
      console.error("Error fetching facilities data:", error);
      res.status(500).send({ error: "Internal server error" });
    }
  };
};

function toFacilityResponse(data: Facility): FacilityResponse {
  return {
    ...data,
    type: FacilityType[data.type],
  };
}
