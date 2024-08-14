CREATE OR REPLACE PROCEDURE public.create_sendbox(IN inviteid uuid, IN martid uuid, IN userid uuid)
 LANGUAGE plpgsql
AS $procedure$
BEGIN
    INSERT INTO public.sendbox (invite_id, mart_id, user_id)
    VALUES (inviteid, martid, userid);
	
EXCEPTION
	WHEN others THEN
		ROLLBACK;
		RAISE;
END;
$procedure$
;
