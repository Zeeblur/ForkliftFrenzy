GIT RULES:

Clone:
git clone https://github.com/Zoeoeh/ForkliftFrenzy.git

Branch:
git checkout -b (NAME OF NEW BRANCH) (NAME OF BRANCH FROM (master))

E.g -> git checkout -b developmentZ master

Add files and commit as normal to dev branch:
git add *
git commit -m "my awesome feature"
git push

When features finished. Switch back to main branch when ready to merge. ONLY PRODUCTION READY. DO NOT BREAK MASTER BUILD PLEASE
git checkout master
git merge --no-ff developmentZ


When finished own work, whilst still on own branch:
* Push and update my own branch as normal
* git checkout master
* git pull
* git checkout mattDev
* git -abort if you're scared


--no-ff : No fast forward. keeps branch history, don't forget this!

Matt's branch : mattDev