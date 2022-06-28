export interface PostVM {
  id: string;
  title: string;
  body: string;
  created: Date;
  updated: Date;
  isPrivate: boolean;

  authorAlias: string;
}