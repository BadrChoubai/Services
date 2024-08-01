package config

import (
	"encoding/json"
	"os"
)

const filePath = "config.json"

func Parse() (*Config, error) {
	file, err := os.Open(filePath)
	if err != nil {
		return nil, err
	}

	defer file.Close()
	decoder := json.NewDecoder(file)
	cfg := Config{}

	err = decoder.Decode(&cfg)
	if err != nil {
		return nil, err
	}

	return &cfg, nil
}
