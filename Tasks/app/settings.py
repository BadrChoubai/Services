from pydantic_settings import BaseSettings


class Settings(BaseSettings):
  PROJECT_NAME: str
  DOCS_URL: str = "/docs"


settings = Settings() # type: ignore
