
env.wsPath = "C:\\Jenkins_workspace\\test-pipeline"
env.GenerateBuildVersion = "0.0.0"
env.GenerateBuildStage = "0"
//if GenerateBuildRelease == 1 -- sing the installers
env.GenerateBuildRelease = "0"

node('master') {
	ws( env.wsPath ) {
		stage ('Initial stage') {
			if (BRANCH_NAME == "master") {
				env.GenerateBuildStage="qa"
				env.GenerateBuildRelease="0"
			}
			if (BRANCH_NAME == "release") {
				env.GenerateBuildStage="rc"
				env.GenerateBuildRelease="0"
			}
			if (BRANCH_NAME.startsWith('r-')) {
				env.GenerateBuildStage=""
				env.GenerateBuildRelease="1"
			}
			bat """
				@echo off
				echo Current branch name is %BRANCH_NAME%
				echo GenerateBuildStage == %GenerateBuildStage%
				echo GenerateBuildRelease == %GenerateBuildRelease%
			"""

		}
	}
}

//clone repos and checkout
if (BRANCH_NAME == "master" || BRANCH_NAME == "release" || BRANCH_NAME.startsWith('r-')) {
	node('master') {
		ws( env.wsPath ) {
			stage('Checkout on master.') {
				def scmVars = checkout scm
				env.GIT_URL = scmVars.GIT_URL
			}
		}
	}
}	

//Versioning
// master release stage
if (BRANCH_NAME == "master" || BRANCH_NAME == "release" ) {
	node('master') {
		ws( env.wsPath ) {
			stage('Versioning') {
			    bat """
				    @echo off
					echo ===Versioning===
					SET VERSION_PATH=%WORKSPACE%\\Generate\\include\\version.h
					python %SCRIPTS-DIR%\\add-build-number-to-version-h.py %VERSION_PATH% %BUILD_NUMBER% 2>tmp_version.txt
					::todo /Generate.WebInstaller/Version.cs
					exit 0
					"""
				env.GenerateBuildVersion=readFile('tmp_version.txt').trim()
				bat """
					@echo off
					echo ==== GenerateBuildVersion = %GenerateBuildVersion% =====
				"""
			}
		}
	}

}
//Versioning
// r- stage
if (BRANCH_NAME.startsWith('r-')) {
	node('master') {
		ws( env.wsPath ) {
			stage('Versioning') {
				env.GenerateBuildVersion=BRANCH_NAME.replace('r-','')
				env.bn=GenerateBuildVersion.split("\\.")[2]
				bat """
					@echo off
					SET VERSION_PATH=%WORKSPACE%\\Generate\\include\\version.h
					python %SCRIPTS-DIR%\\add-build-number-to-version-h.py %VERSION_PATH% $env.bn 2>tmp_version.txt
					::todo /Generate.WebInstaller/Version.cs
					echo ==== GenerateBuildVersion = %GenerateBuildVersion% ====
					exit 0
				"""
			}
		}
	}
}

//TODO packing and copy repo to slave
//TODO building on slave
//TODO copying results to s3 (from slave or from master ? )



if (BRANCH_NAME == "release") {
	node ('master') {
		ws (env.wsPath) {
			stage ('Push new tag to GitHub') {
			    env.GIT_URL = env.GIT_URL.replace('https://','')
				bat 'echo prepare new release tag'
					withCredentials([usernamePassword(credentialsId: 'vgorbulenko_token_github', passwordVariable: 'USERPASS', usernameVariable: 'USERNAME')]) {
						bat """
							@echo off
							git config --global user.email "generate-ci@frustum.io"
							git config --global user.name "Generate CI"
							git tag -a rc-$env.GenerateBuildVersion -m \'autotag\'
							git push https://%USERPASS%@%GIT_URL% rc-$env.GenerateBuildVersion
						"""
                    }

				
			}		
		}
	}
}


if ( BRANCH_NAME.startsWith('r-') ) {
	node ('master') {
		ws( env.wsPath ) {
			stage ('Publishing installers to PROD bucket') {
				bat """
					echo THIS IS THE TAAAAAAAAAAAAAAAAAAAAAAG!!!!!!!!!! === $BRANCH_NAME ===
				"""
			}
		}
	}
}



//node('slave') {
  //  ws( env.wsPath ) {
	//    bat """ %SCRIPTS-DIR%\\sign.bat generate C:\\test\\certificate\\test_sign_code_sert.pfx passw1234ord"""
//		bat """   %SCRIPTS-DIR%\\sign.bat msi C:\\test\\certificate\\test_sign_code_sert.pfx passw1234ord"""
	//	bat """   %SCRIPTS-DIR%\\sign.bat package C:\\test\\certificate\\test_sign_code_sert.pfx passw1234ord"""	
//	}
//}