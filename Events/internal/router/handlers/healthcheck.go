package handlers

import (
	"net/http"
)

const (
	HealthcheckResponseStr string = "Healthy"
)

func Healthcheck() http.HandlerFunc {
	return func(w http.ResponseWriter, r *http.Request) {
		err := WriteResponse(w, http.StatusOK, HealthcheckResponseStr, nil)

		if err != nil {
			BadRequestResponse(w, err)
		}
	}
}
