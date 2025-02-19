import { Facility } from "../../services/index.js";
import { Service } from "../../services/Service.js";
import { FacilityResponse, FacilityType } from "../../services/facilities.js";
import { transformResponse } from "../../lib/transformResponse.js";

export const getFacilityById = (facilitiesService: Service<Facility>) => {
  return async function (req, res) {
    const { id: facilityId } = req.params;
    let data: FacilityResponse;

    try {
      const facility = facilitiesService.getById(facilityId);
      if (!facility) {
        return res
          .status(404)
          .send({ error: `Facility with given ID not found` });
      }

      data = transformResponse<Facility, FacilityResponse>(
        facility,
        toFacilityResponse,
      );

      res.status(200).send(data);
    } catch (error) {
      console.error("Error fetching facility details:", error);
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
