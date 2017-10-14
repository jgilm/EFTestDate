# EFTestDate

Repo project for DateTimeOffset issue

See Issue: https://github.com/npgsql/Npgsql.EntityFrameworkCore.PostgreSQL/issues/250

The essential issue appears to be that when querying against a `DateTimeOffset` column, invalid SQL is generated
