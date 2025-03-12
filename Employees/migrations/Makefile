####################################################################################################
# Database Tasks
####################################################################################################

db/info: # @HELP prints value of DB_CONNECTION_STRING
db/info:
	@echo ${DB_CONNECTION_STRING}

db/version: # @HELP prints current migration status
db/version:
	migrate -database ${DB_CONNECTION_STRING} -verbose version

db/connect: # @HELP connect to locally running database
db/connect:
	sqlite3 ${DB_CONNECTION_STRING}

db/migrations/up: # @HELP run migrations to create database tables
db/migrations/up:
	migrate -path ./migrations -database ${DB_CONNECTION_STRING} up


db/migrations/down: # @HELP run migrations to teardown database tables
db/migrations/down:
	migrate -path ./migrations -database ${DB_CONNECTION_STRING} down


db/migrations/refresh: # @HELP refresh database tables
db/migrations/refresh: db/migrations/down db/migrations/up
