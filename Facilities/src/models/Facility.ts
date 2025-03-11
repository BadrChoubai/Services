import { Location } from "./Location.js";

export enum FacilityType {
  "SortCenter",
  "DistributionCenter",
}

export type FacilityDbResponse = {
  id: string;
  alias: string;
  type: FacilityType;
  locationId: string;
  address: string;
  city: string;
  stateCode: string;
  postalCode: string;
  countryCode: string;
};

export type Facility = {
  id: string;
  alias: string;
  type: FacilityType;
  locationId: string;
  location?: Location;
};

export type FacilityResponse = {
  alias: string;
  id: string;
  location: Location;
  type: string;
};
