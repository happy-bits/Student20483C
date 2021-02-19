sqlcmd -S .\sqlexpress -i "%cd%\SchoolGradesDB.sql" -v input="%cd%"
