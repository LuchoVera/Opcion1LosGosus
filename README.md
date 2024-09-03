# Los Gosus - Library Project

| Members                             | New Members
| ---                                 | ---
| Luis Enrique Espinoza Vera          | Alex Paca Meneses
| Sebasthian Khristian Salinas Pozzo  | José Luis Terán Rocha
| Josue Mauricio Prado Camacho        | Ronaldo Mendoza Mallcu
| Ignacio Ruben Villarroel Rodriguez  |

## Tasks to be worked on:

## Alex Paca Meneses
| Ticket                                              | Status
| ---                                                 | ---
| Bugfix: Delete validation for null books or patrons | **Done**
| Bugfix: Generation of empty reports                 | **Done**
| Bugfix: Update non existent Patron or Book          | **Done**
| Refactor: Book and Patron manager show methods      | **Done**
| Refactor: Merge validations for same methods        | **Done**

## José Luis Terán
| Ticket                                              | Status
| ---                                                 | ---
| UML: Migrate UML diagram                            | **Done**
| CI: Implementing C# conventions                     | **Done**
| CI: Workflows and VS Code Extensions                | **Done**
| Bugfix: Unit test for controllers                   | **Done**
| Build: Refactor Project Structure                   | **Done** but not merged

## Ronaldo Mendoza
| Ticket                                                  | Status
| ---                                                     | ---
| Refactor: Application of design pattern in validations  | **Done**
| Refactor: Update tests for validations                  | **Done**

## Development Team Conventions

### Branch Naming Convention

To ensure consistency and clarity in Git branch names, we will follow the convention below:

#### Category

A git branch should start with a category. Choose one of the following:

- **feature**: For adding, refactoring, or removing a feature.
- **bugfix**: For fixing a bug.
- **hotfix**: For changing code with a temporary solution and/or without following the usual process (typically due to an emergency).
- **test**: For experimenting outside of an issue/ticket.

#### Reference

After the category, there should be a "/" followed by the reference of the issue/ticket you are working on. If there’s no reference, use `no-ref`.

#### Description

After the reference, there should be another "/" followed by a description that sums up the purpose of this specific branch. This description should be short and written in "kebab-case."

By default, you can use the title of the issue/ticket you are working on. Just replace any special character with "-".

#### Examples

- **Adding, refactoring, or removing a feature:**  
  `git branch feature/feat-42/create-new-button-component`

- **Fixing a bug:**  
  `git branch bugfix/feat-342/button-overlap-form-on-mobile`

- **Quickly fixing a bug (possibly with a temporary solution):**  
  `git branch hotfix/no-ref/registration-form-not-working`

- **Experimenting outside of an issue/ticket:**  
  `git branch test/no-ref/refactor-components-with-atomic-design`

### Commit Naming Convention

For commit messages, we will combine and simplify the Angular Commit Message Guideline and the Conventional Commits guideline.

#### Category

A commit message should start with a category of change. You can use the following four categories for nearly everything:

- **feat**: For adding a new feature.
- **fix**: For fixing a bug.
- **refactor**: For changing code for performance or convenience purposes (e.g., readability).
- **chore**: For everything else (writing documentation, formatting, adding tests, cleaning up unused code, etc.).

After the category, there should be a ":" followed by the commit description.

#### Statement(s)

After the colon, the commit description should consist of short statements describing the changes.

Each statement should start with a verb in the imperative form. Statements should be separated by a ";" if there are multiple.

#### Examples

- `git commit -m 'feat: add new button component; add new button components to templates'`
- `git commit -m 'fix: add the stop directive to button component to prevent propagation'`
- `git commit -m 'refactor: rewrite button component in TypeScript'`
- `git commit -m 'chore: write button documentation'`

### Trello Card Management

For managing tasks in Trello, we will follow this workflow:

1. **Backlog:** Tasks start in the backlog.
2. **In Progress:** When you pick a task to work on, move it to the "In Progress" column.
3. **Code Review:** Once the task is complete, move it to the "Code Review" column for review.
4. **Done:** If the pull request is approved, move the card to the "Done" column.
5. **Review Not Approved:** If the pull request is not approved, the reviewer will move the card back to "In Progress" for necessary corrections.
