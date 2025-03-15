import uuid

from pydantic import BaseModel
from pydantic.v1.utils import to_lower_camel
from sqlmodel import Field, SQLModel


class Employee(SQLModel):
    employee_id: uuid.UUID
    facility_id: uuid.UUID
    first_name: str
    last_name: str

    class Config:
        alias_generator = to_lower_camel
        populate_by_name = True


class TaskCreate(SQLModel):
    employee_id: uuid.UUID = Field(
        nullable=False, description="Employee assigned to task"
    )
    task_description: str = Field(
        nullable=False, max_length=255, description="Task description"
    )
    duration: int = Field(default=14400, gt=0, description="Duration of task")

    class Config:
        alias_generator = to_lower_camel
        populate_by_name = True


class TaskCreated(BaseModel):
    task_id: uuid.UUID = Field(max_length=36, primary_key=True)
    message: str = Field(
        max_length=120,
        description="Message describing the task creation result",
    )
    task_description: str = Field(
        max_length=255, description="Description of task assigned to employee"
    )
    duration: int = Field(
        default=14400, gt=0, description="Duration of task in seconds"
    )
    employee: Employee

    class Config:
        alias_generator = to_lower_camel
        populate_by_name = True
