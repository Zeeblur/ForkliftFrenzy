GIT RULES:

Clone:
git clone https://github.com/Zoeoeh/ForkliftFrenzy.git

Large File Storage:
Download link:
https://git-lfs.github.com/

git lfs install

Track files, e.g for sounds or models. These should be really stored in the drive anyway... not on git
git lfs track "*.wav"

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

--no-ff : No fast forward. keeps branch history, don't forget this!

Matt's branch : mattDev