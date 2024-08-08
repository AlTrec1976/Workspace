CREATE OR REPLACE PROCEDURE public.create_user(
	userlogin character,
	userpassword character,
	username character,
	usersurname character)
    LANGUAGE 'plpgsql'

AS $BODY$
BEGIN
INSERT INTO public.users (login, password, name, surname) VALUES (userlogin, userpassword, username, usersurname);
END;
$BODY$;