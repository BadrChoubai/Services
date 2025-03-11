import { FacilitiesService } from "../../service/facilities.js";
import {
  FacilityDbResponse,
  FacilityResponse,
  FacilityType,
} from "../../models/Facility.js";
import { transformResponse } from "../../lib/transformResponse.js";

export const getFacility = (facilitiesService: FacilitiesService) => {
  return async function (req, res) {
    try {
      const { id } = req.params;
      const facility = await facilitiesService.getOne(id);

      if (!facility) {
        return res.status(404).send({
          error: `Facility with given id ${id} not found`,
        });
      }

      const data = transformResponse<unknown, FacilityResponse>(
        facility,
        toFacilityResponse,
      );

      res.status(200).send(data);
    } catch (error) {
      console.error("Error fetching facilities data:", error);
      res.status(500).send({ error: "Internal server error" });
    }
  };
};

function toFacilityResponse(data: FacilityDbResponse): FacilityResponse {
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
