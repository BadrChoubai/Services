import { FacilitiesService } from "../../service/facilities.js";
import {
  FacilityDbResponse,
  FacilityResponse,
  FacilityType,
} from "../../models/Facility.js";
import { transformResponse } from "../../lib/transformResponse.js";

export const getFacilities = (facilitiesService: FacilitiesService) => {
  return async function (req, res) {
    try {
      const facilities = await facilitiesService.getAll();

      const facilitiesListResponse: FacilityResponse[] = facilities.map(
        (facility: FacilityDbResponse): FacilityResponse =>
          transformResponse<FacilityDbResponse, FacilityResponse>(
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

function toFacilityResponse(data: FacilityDbResponse): FacilityResponse {
  console.log(data);
  return {
    id: data.id,
    alias: data.alias,
    type: FacilityType[data.type],
    location: {
      id: data.locationId,
      city: data.city,
      address: data.address,
      stateCode: data.stateCode,
      postalCode: data.postalCode,
      countryCode: data.countryCode,
    },
  };
}
