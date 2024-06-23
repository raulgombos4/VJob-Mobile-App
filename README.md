# VJob

Link gitlab.upt.ro

Comenzi git:

To upload your changes to the GitHub repository using branches, follow these steps:

1. **Clone the Repository (if you haven't already):**
   If you haven't cloned the repository yet, you can do it with the following command:

   ```bash
   git clone <repository_url>
   ```

   Replace `<repository_url>` with the URL of your GitHub repository.

2. **Navigate to the Project Directory:**
   Use the `cd` command to navigate to the project directory:

   ```bash
   cd <project_directory>
   ```

   Replace `<project_directory>` with the name of the directory where you cloned the repository.

3. **Checkout the Branch:**
   To work on a specific branch, use the `git checkout` command:

   ```bash
   git checkout <branch_name>
   ```

   Replace `<branch_name>` with the name of the branch you want to work on. For example, if you want to work on the "backend-feature" branch, use:

   ```bash
   git checkout backend-feature
   ```

4. **Make and Commit Changes:**
   Make your code changes in the working directory. Once you're satisfied with your changes, commit them:

   ```bash
   git add .
   git commit -m "Description of your changes"
   ```

   Replace "Description of your changes" with a brief, meaningful description of what you've changed or added.

5. **Push Your Branch to GitHub:**
   To upload your changes to GitHub on your specific branch, use the `git push` command:

   ```bash
   git push origin <branch_name>
   ```

   Replace `<branch_name>` with the name of the branch you're working on. For example:

   ```bash
   git push origin backend-feature
   ```

   This will push your changes to the branch on the GitHub repository.

6. **Create a Pull Request:**
   After pushing your changes, go to the GitHub repository in your web browser and navigate to the branch you just pushed to. GitHub will usually suggest creating a Pull Request (PR). Follow the prompts to create a PR.

7. **Review and Merge:**
   Your team members can review the changes in the PR. If everything looks good and passes any required checks (e.g., CI/CD pipelines), you can merge the changes into the main branch.

8. **Pull the Latest Changes:**
   To ensure you have the latest code on your local machine, pull the changes from the main branch:

   ```bash
   git checkout main
   git pull origin main
   ```
