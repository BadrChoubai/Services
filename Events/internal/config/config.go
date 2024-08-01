package config

type (
	Service struct {
		Port int `json:"httpPort"`
	}

	Config struct {
		Service
	}
)

func Load() *Config {
	cfg, err := Parse()
	if err != nil {
		return nil
	}

	return cfg
}
