
env.wsPath = "C:\\Jenkins_workspace\\test-pipeline"

//def receive
  event_type = request.headers["X-GitHub-Event"]
//  payload    = request.body

//env.eventType = event_type

node('master') {
    ws( env.wsPath ) {
        stage('Checkout on master') {
			checkout scm
//            checkout([$class: 'GitSCM', branches: [[name: BRANCH_NAME]], doGenerateSubmoduleConfigurations: false, extensions: [], submoduleCfg: [], userRemoteConfigs: [[credentialsId: 'github-ssh', url: 'git@github.com:frustumInc/generate.git']]]) 
//          checkout([$class: 'GitSCM', branches: [[name: gitlabSourceBranch]], doGenerateSubmoduleConfigurations: false, extensions: [], submoduleCfg: [], userRemoteConfigs: [[credentialsId: 'vgorbulenko_https_github', url: 'https://gitlab.amcbridge.com/spronyuk/pipeline-dev-repo']]])
		}

        stage('test stage. Looking for variables') {
			bat """ echo env.BRANCH_NAME = %env.BRANCH_NAME% """
			bat """ echo env.GIT_BRANCH = %env.GIT_BRANCH% """
			//bat """ echo env.eventType = %env.eventType%  | ${env.eventType}    """
			bat """ echo payload = ${payload.ref} """
			bat """ echo -------------- """
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