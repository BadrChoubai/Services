from fastapi import FastAPI

from app.api.main import api_router
from app.settings import settings

app = FastAPI(
    title=settings.PROJECT_NAME,
    docs_url=settings.DOCS_URL,
)

app.include_router(api_router)
