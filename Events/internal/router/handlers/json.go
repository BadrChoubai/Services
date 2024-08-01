package handlers

import (
	"encoding/json"
	"net/http"
)

func WriteResponse[T any](w http.ResponseWriter, status int, data T, headers http.Header) error {
	JSON, err := json.MarshalIndent(data, "", "\t")
	if err != nil {
		return err
	}

	JSON = append(JSON, '\n')

	for key, value := range headers {
		w.Header()[key] = value
	}

	w.Header().Set("Content-Type", "application/json")
	w.WriteHeader(status)
	_, err = w.Write(JSON)

	if err != nil {
		return err
	}

	return nil
}

func BadRequestResponse(w http.ResponseWriter, err error) {
	ErrorResponse(w, http.StatusBadRequest, err.Error(), nil)
}

func ErrorResponse[T any](w http.ResponseWriter, status int, data T, headers http.Header) {
	err := WriteResponse(w, status, data, headers)
	if err != nil {
		w.WriteHeader(http.StatusInternalServerError)
	}
}
