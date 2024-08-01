package handlers

import "net/http"

func NotFound(w http.ResponseWriter, r *http.Request) {
	w.WriteHeader(404)
	w.Write([]byte("route does not exist"))
}

func MethodNotAllowed(w http.ResponseWriter, r *http.Request) {
	w.WriteHeader(405)
	w.Write([]byte("method is not valid"))
}
