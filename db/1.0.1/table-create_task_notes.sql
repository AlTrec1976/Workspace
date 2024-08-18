CREATE TABLE public.task_notes (
                                   taskid uuid NOT NULL,
                                   noteid uuid NOT NULL
);

ALTER TABLE public.task_notes ADD CONSTRAINT task_task_fk FOREIGN KEY (taskid) REFERENCES public.tasks(id);
ALTER TABLE public.task_notes ADD CONSTRAINT task_note_fk FOREIGN KEY (noteid) REFERENCES public.notes(id);
