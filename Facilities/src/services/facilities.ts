import { Service } from "./Service.js";
import type { Location } from "./locations.js";

export enum FacilityType {
  "SortCenter",
  "DistributionCenter",
}

export type Facility = {
  alias: string;
  id: string;
  location: Location;
  type: FacilityType;
};

export type FacilityResponse = {
  alias: string;
  id: string;
  location: Location;
  type: string;
};

export type FacilitiesListResponse = FacilityResponse[];

export const inMemoryFacilitiesStore: Facility[] = [
  {
    alias: "BDU5",
    id: "1e95757d-066d-41a6-b5d2-5e34b71d6051",
    location: {
      id: "f103279-fe1c-4d68-8043-09ad13f13207",
      address: "8350 Quintero St",
      city: "Commerce City",
      stateCode: "CO",
      postalCode: "80022",
      countryCode: "US",
    },
    type: FacilityType.SortCenter,
  },
];

export class FacilitiesService extends Service<Facility> {
  constructor(facilitiesStore: Facility[]) {
    super(facilitiesStore);
  }
}
