package data

import "github.com/google/uuid"

type Employee struct {
  Id        uuid.UUID `json:"id"`
  FirstName string    `json:"firstName"`
  LastName  string    `json:"lastName"`
  Facility  Facility  `json:"facility"`
}

type Facility struct {
  Id       uuid.UUID    `json:"id"`
  Alias    string       `json:"alias"`
  Type     FacilityType `json:"type"`
  Location Location     `json:"location"`
}

type FacilityType int64

const (
  SortCenter FacilityType = iota + 1
  DistributionCenter
)

type Location struct {
  Id          string `json:"id"`
  Address     string `json:"address"`
  City        string `json:"city"`
  StateCode   string `json:"stateCode"`
  PostalCode  string `json:"postalCode"`
  CountryCode string `json:"countryCode"`
}
