CREATE OR REPLACE PROCEDURE public.create_user(IN userlogin character, IN userpassword character, IN username character, IN usersurname character)
 LANGUAGE plpgsql
AS $procedure$
BEGIN
INSERT INTO public.users (login, password, name, surname) VALUES (userlogin, userpassword, username, usersurname);
END;
$procedure$;