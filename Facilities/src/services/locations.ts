import { Service } from "./Service.js";

export type Location = {
  id: string;
  address: string;
  city: string;
  stateCode: string;
  postalCode: string;
  countryCode: string;
};

export const inMemoryLocationsStore: Location[] = [
  {
    id: "f103279-fe1c-4d68-8043-09ad13f13207",
    address: "8350 Quintero St",
    city: "Commerce City",
    stateCode: "CO",
    postalCode: "80022",
    countryCode: "US",
  },
];

export class LocationsService extends Service<Location> {
  constructor(locationsStore: Location[]) {
    super(locationsStore);
  }
}
