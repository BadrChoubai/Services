from fastapi import FastAPI

from app.settings import settings
from app.api.main import api_router

app = FastAPI(
    title=settings.PROJECT_NAME,
    docs_url=settings.DOCS_URL,
)

app.include_router(api_router)
