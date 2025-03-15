from fastapi import APIRouter

from app.api.models import TaskCreate, TaskCreated

router = APIRouter(prefix="/api", tags=["api"])


@router.post("/assign", response_model=TaskCreated)
async def create_task(*, task: TaskCreate) -> None:
    print(task)
