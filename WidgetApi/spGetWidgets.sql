DROP PROCEDURE [dbo].[spGetWidgets]
GO

CREATE PROCEDURE [dbo].[spGetWidgets]
    @PageNumber           ut_StandardInteger = NULL
    , @PageSize           ut_StandardInteger = NULL
    , @SortColumn         NVARCHAR(MAX) = NULL
    , @SortDirection      NVARCHAR(MAX) = NULL
    , @ReturnJSON         ut_StandardJSON OUTPUT
AS
BEGIN
    SET @returnJSON =
    (SELECT
        [Id]
        , [Name]
        , [Shape]
        , [Color]
    FROM
        dbo.[Widget]
    ORDER BY
        CASE WHEN @SortColumn = 'Name' AND @SortDirection = 'ASC' THEN [Name] END ASC,
        CASE WHEN @SortColumn = 'Name' AND @SortDirection = 'DESC' THEN [Name] END DESC,
        CASE WHEN @SortColumn = 'Shape' AND @SortDirection = 'ASC' THEN [Shape] END ASC,
        CASE WHEN @SortColumn = 'Shape' AND @SortDirection = 'DESC' THEN [Shape] END DESC,
        CASE WHEN @SortColumn = 'Color' AND @SortDirection = 'ASC' THEN [Color] END ASC,
        CASE WHEN @SortColumn = 'Color' AND @SortDirection = 'DESC' THEN [Color] END DESC
    OFFSET @PageNumber ROWS
    FETCH NEXT @PageSize ROWS ONLY
	FOR JSON PATH, INCLUDE_NULL_VALUES
    );
END
GO

