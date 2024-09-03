# Los Gosus - Library Proyect
## Members:
* Luis Enrique Espinoza Vera
* Sebasthian Khristian Salinas Pozzo
* Josue Mauricio Prado Camacho
* Ignacio Ruben Villarroel Rodriguez

## new Members:
* Alex Paca Meneses
* Jose Luis Teran Rocha
* Ronaldo Mendoza Mallcu

## Tasks to be worked on

## Alex Paca Meneses
* Bugfix: Delete validation for null books or patrons | status "done"
* Bugfix: Generation of empty reports | status "done"
* Bugfix: Update non existent Patron or Book | status "done"
* Refactor: Book and Patron manager show methods | status "done"
* Refactor: Merge validations for same methods | status "done"

## Jose Luis Teran
* UML: Migrate UML diagram | status "done"
* CI: Implementing C# conventions | status "done"
* CI: Workflows and VS Code Extensions | status "done"
* Bugfix: Unit test for controllers | status "done"

## Ronaldo Mendoza
* Refactor: Application of design pattern in validations | status "done"
* Refactor: Update tests for validations | status "done"

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
