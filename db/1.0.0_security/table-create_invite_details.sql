CREATE TABLE public.invite_details (
	invite_id uuid NOT NULL,
	user_id uuid NOT NULL,
	user_comments varchar(200) NULL
);

ALTER TABLE public.invite_details ADD CONSTRAINT invite_details_invites_fk FOREIGN KEY (invite_id) REFERENCES public.invites(id);
ALTER TABLE public.invite_details ADD CONSTRAINT invitea_users_fk FOREIGN KEY (user_id) REFERENCES public.users(id);