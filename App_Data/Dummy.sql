-- Inserting dummy data for the past 2 months
DECLARE @StartDate DATE = DATEADD(MONTH, -2, GETDATE());
DECLARE @EndDate DATE = GETDATE();

WHILE @StartDate <= @EndDate
BEGIN
    -- Generate random status: Present or Absent
    DECLARE @Status VARCHAR(20);
    SET @Status = CASE WHEN RAND() > 0.5 THEN 'Present' ELSE 'Absent' END;

    -- Insert record for the current date
    INSERT INTO AttendanceRecords (UserID, AttendanceDate, AttendanceTime, Status)
    VALUES (9, @StartDate, CASE WHEN @Status = 'Present' THEN CONVERT(TIME, GETDATE()) ELSE NULL END, @Status);

    -- Move to the next day
    SET @StartDate = DATEADD(DAY, 1, @StartDate);
END;
